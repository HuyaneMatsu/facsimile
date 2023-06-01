__all__ = ('get_face_position',)

from math import atan2, tau

from ....helpers import get_point_difference_3d, get_point_average_2d
from ....points.face import POINT_FACE__FACE_LEFT_MOST, POINT_FACE__FACE_RIGHT_MOST

from .constants import (
    FACE_POSITION__X__MIN, FACE_POSITION__X__MAX, FACE_POSITION__Y__MIN, FACE_POSITION__Y__MAX, FACE_POSITION__Z__MIN,
    FACE_POSITION__Z__MAX
)


def get_face_position(landmarks):
    left = landmarks[POINT_FACE__FACE_LEFT_MOST]
    right = landmarks[POINT_FACE__FACE_RIGHT_MOST]
    
    face_width = get_point_difference_3d(left, right)
    # As the head rotates some why it's size increases, so we will reduce it
    rotation_reduction = 1.0 + abs(atan2(left.y - right.y, left.x - right.x) / tau)
    
    face_width /= rotation_reduction
    
    face_position_z = -1.0 / face_width
    if face_position_z < FACE_POSITION__Z__MIN:
        face_position_z = FACE_POSITION__Z__MIN
    elif face_position_z > FACE_POSITION__Z__MAX:
        face_position_z = FACE_POSITION__Z__MAX
    
    face_middle_x, face_middle_y = get_point_average_2d(left, right)
    
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
