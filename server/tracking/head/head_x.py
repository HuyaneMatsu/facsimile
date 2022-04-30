__all__ = ('get_head_x_rotation',)

from ..face_mesh_points import (
    FACE_MESH_POINT__FACE_LEFT_MOST, FACE_MESH_POINT__FACE_RIGHT_MOST, FACE_MESH_POINT__NOSE_TIP
)


def get_head_x_rotation(landmarks):
    mid_x = landmarks[FACE_MESH_POINT__NOSE_TIP].x
    left_x = landmarks[FACE_MESH_POINT__FACE_LEFT_MOST].x
    # Left is higher than right
    right_x = landmarks[FACE_MESH_POINT__FACE_RIGHT_MOST].x
    
    if (left_x > mid_x) and (mid_x > right_x):
        relative_left_x = left_x - mid_x
        relative_right_x = mid_x - right_x
        rotate_x =  90.0 * (relative_left_x / (relative_left_x + relative_right_x) - 0.5)
        
    elif (left_x == mid_x):
        rotate_x = +45.0
    
    elif (right_x == mid_x):
        rotate_x = 45.0
    
    elif (left_x < mid_x):
        difference_points_x = left_x - right_x
        difference_middle_x = mid_x - left_x
        rotate_x =  -45.0 * (difference_middle_x / (difference_points_x + difference_middle_x)) - 45.0
    
    else:
        difference_points_x = left_x - right_x
        difference_middle_x = right_x - mid_x
        rotate_x =  45.0 * (difference_middle_x / (difference_points_x + difference_middle_x)) + 45.0
    
    return rotate_x
