__all__ = ('get_mouth_openness',)

from ..face_mesh_points import (
    FACE_MESH_POINT__LIPS_BOT, FACE_MESH_POINT__LIPS_LEFT, FACE_MESH_POINT__LIPS_TOP, FACE_MESH_POINT__LIPS_RIGHT,
    FACE_MESH_POINT__FACE_LEFT_MOST, FACE_MESH_POINT__FACE_RIGHT_MOST, FACE_MESH_POINT__MOUTH_TOP,
    FACE_MESH_POINT__MOUTH_BOT
)
from ..helpers import get_point_difference_3d

from .constants import OPENNESS_MULTIPLIER_X, OPENNESS_MULTIPLIER_Y, OPENNESS_MAX


def get_mouth_openness(landmarks):
    most_difference = get_point_difference_3d(
        landmarks[FACE_MESH_POINT__FACE_LEFT_MOST],
        landmarks[FACE_MESH_POINT__FACE_RIGHT_MOST],
    )
    
    mouth_difference_x = get_point_difference_3d(
        landmarks[FACE_MESH_POINT__LIPS_LEFT],
        landmarks[FACE_MESH_POINT__LIPS_RIGHT],
    )
    
    mouth_difference_y = 0.5 * (
        get_point_difference_3d(
            landmarks[FACE_MESH_POINT__LIPS_TOP],
            landmarks[FACE_MESH_POINT__LIPS_BOT],
        )
        +
        get_point_difference_3d(
            landmarks[FACE_MESH_POINT__MOUTH_TOP],
            landmarks[FACE_MESH_POINT__MOUTH_BOT],
        )
    )
    
    mouth_openness_x = mouth_difference_x / most_difference * OPENNESS_MULTIPLIER_X
    mouth_openness_y = mouth_difference_y / most_difference * OPENNESS_MULTIPLIER_Y
    
    if mouth_openness_x > OPENNESS_MAX:
        mouth_openness_x = OPENNESS_MAX
    
    if mouth_openness_y > OPENNESS_MAX:
        mouth_openness_y = OPENNESS_MAX
    
    return mouth_openness_x, mouth_openness_y
