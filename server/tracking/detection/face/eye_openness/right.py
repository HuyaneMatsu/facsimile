__all__ = ('get_right_eye_openness',)

from ....points.face import (
    POINT_FACE__EYE_OUTLINE_RIGHT__BOT, POINT_FACE__EYE_OUTLINE_RIGHT__INNER_2, POINT_FACE__EYE_OUTLINE_RIGHT__OUTER_2,
    POINT_FACE__EYE_OUTLINE_RIGHT__TOP
)

from .helpers import get_eye_openness


def get_right_eye_openness(landmarks):
    return get_eye_openness(
        landmarks[POINT_FACE__EYE_OUTLINE_RIGHT__INNER_2],
        landmarks[POINT_FACE__EYE_OUTLINE_RIGHT__OUTER_2],
        landmarks[POINT_FACE__EYE_OUTLINE_RIGHT__BOT],
        landmarks[POINT_FACE__EYE_OUTLINE_RIGHT__TOP],
    )
