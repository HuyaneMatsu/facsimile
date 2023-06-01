__all__ = ('get_mouth_openness',)

from ....helpers import get_point_difference_3d
from ....points.face import (
    POINT_FACE__LIPS_BOT, POINT_FACE__LIPS_LEFT, POINT_FACE__LIPS_TOP, POINT_FACE__LIPS_RIGHT,
    POINT_FACE__FACE_LEFT_MOST, POINT_FACE__FACE_RIGHT_MOST, POINT_FACE__MOUTH_TOP,
    POINT_FACE__MOUTH_BOT
)

from .constants import MOUTH_OPENNESS_MULTIPLIER_X, MOUTH_OPENNESS_MULTIPLIER_Y, MOUTH_OPENNESS_MAX


def get_mouth_openness(landmarks):
    most_difference = get_point_difference_3d(
        landmarks[POINT_FACE__FACE_LEFT_MOST],
        landmarks[POINT_FACE__FACE_RIGHT_MOST],
    )
    
    mouth_difference_x = get_point_difference_3d(
        landmarks[POINT_FACE__LIPS_LEFT],
        landmarks[POINT_FACE__LIPS_RIGHT],
    )
    
    mouth_difference_y = 0.5 * (
        get_point_difference_3d(
            landmarks[POINT_FACE__LIPS_TOP],
            landmarks[POINT_FACE__LIPS_BOT],
        )
        +
        get_point_difference_3d(
            landmarks[POINT_FACE__MOUTH_TOP],
            landmarks[POINT_FACE__MOUTH_BOT],
        )
    )
    
    mouth_openness_x = mouth_difference_x / most_difference * MOUTH_OPENNESS_MULTIPLIER_X
    mouth_openness_y = mouth_difference_y / most_difference * MOUTH_OPENNESS_MULTIPLIER_Y
    
    if mouth_openness_x > MOUTH_OPENNESS_MAX:
        mouth_openness_x = MOUTH_OPENNESS_MAX
    
    if mouth_openness_y > MOUTH_OPENNESS_MAX:
        mouth_openness_y = MOUTH_OPENNESS_MAX
    
    return mouth_openness_x, mouth_openness_y
