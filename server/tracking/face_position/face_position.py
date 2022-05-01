__all__ = ('get_face_position',)

from ..face_mesh_points import FACE_MESH_POINT__FACE_LEFT_MOST, FACE_MESH_POINT__FACE_RIGHT_MOST
from ..helpers import get_point_difference_3d, point_average_2d

from .constants import (
    FACE_POSITION__X__MIN, FACE_POSITION__X__MAX, FACE_POSITION__Y__MIN, FACE_POSITION__Y__MAX, FACE_POSITION__Z__MIN,
    FACE_POSITION__Z__MAX
)


def get_face_position(landmarks):
    point_left_most = landmarks[FACE_MESH_POINT__FACE_LEFT_MOST]
    point_right_most = landmarks[FACE_MESH_POINT__FACE_RIGHT_MOST]
    
    face_width = get_point_difference_3d(point_left_most, point_right_most)
    
    face_position_z = -1.0 / face_width
    if face_position_z < FACE_POSITION__Z__MIN:
        face_position_z = FACE_POSITION__Z__MIN
    elif face_position_z > FACE_POSITION__Z__MAX:
        face_position_z = FACE_POSITION__Z__MAX
    
        
    face_middle_x, face_middle_y = point_average_2d(point_left_most, point_right_most)
    
    face_position_x = (face_middle_x - 0.5) * face_position_z
    face_position_y = (face_middle_y - 0.5) * face_position_z
    
    if face_position_x < FACE_POSITION__X__MIN:
        face_position_x = FACE_POSITION__X__MIN
    elif face_position_x > FACE_POSITION__X__MAX:
        face_position_x = FACE_POSITION__X__MAX
    
    if face_position_y < FACE_POSITION__Y__MIN:
        face_position_y = FACE_POSITION__Y__MIN
    elif face_position_y > FACE_POSITION__Y__MAX:
        face_position_y = FACE_POSITION__Y__MAX
    
    return face_position_x, face_position_y, face_position_z
