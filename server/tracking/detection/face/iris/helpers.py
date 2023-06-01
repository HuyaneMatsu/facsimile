__all__ = ()

from ....helpers import Vector3, get_point_difference_3d

from .constants import EYE_RATIO_X_SHIFT_SCALING


def scale_iris_rotations(x_ratio, y_ratio):
    x_ratio *= 40.0
    
    if x_ratio > 8.0:
        x_ratio = 8.0
    
    elif x_ratio < -8.0:
        x_ratio = -8.0
    
    y_ratio *= 72.0
    if y_ratio > 10.0:
        y_ratio = 10.0
    elif y_ratio < -10.0:
        y_ratio = -10.0
    
    return x_ratio, y_ratio



def get_iris_rotations(iris, top, bot, inner, outer):
    rotation_x = get_point_reflection_ratio(iris, inner, outer)
    rotation_y = get_point_reflection_ratio(iris, top, bot)
    
    rotation_x = (rotation_x - 0.5) / EYE_RATIO_X_SHIFT_SCALING
    rotation_y -= 0.5
    
    rotation_x, rotation_y = scale_iris_rotations(rotation_x, rotation_y)
    return rotation_x, rotation_y


def get_point_reflection_ratio(mid, point_1, point_2):
    difference = get_point_difference_3d(point_1, point_2)
    
    # These shit types don't support unpacking.
    mid_x = mid.x
    mid_y = mid.y
    mid_z = mid.z
    
    point_1_x = point_1.x
    point_1_y = point_1.y
    point_1_z = point_1.z
    
    point_2_x = point_2.x
    point_2_y = point_2.y
    point_2_z = point_2.z
    
    line_vector_from_mid = (
       (mid_x - point_1_x) * (point_2_x - point_1_x) +
       (mid_y - point_1_y) * (point_2_y - point_1_y) +
       (mid_z - point_1_z) * (point_2_z - point_1_z)
    ) / (difference * difference)
    
    quadrilateral = Vector3(
        ((point_2_x - point_1_x) * line_vector_from_mid + point_1_x),
        ((point_2_y - point_1_y) * line_vector_from_mid + point_1_y),
        ((point_2_z - point_1_z) * line_vector_from_mid + point_1_z),
    )
    
    return get_point_difference_3d(point_1, quadrilateral) / difference
