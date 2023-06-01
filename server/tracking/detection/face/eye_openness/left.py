__all__ = ('get_left_eye_openness',)

from ....points.face import (
    POINT_FACE__EYE_OUTLINE_LEFT__BOT, POINT_FACE__EYE_OUTLINE_LEFT__INNER_2, POINT_FACE__EYE_OUTLINE_LEFT__OUTER_2,
    POINT_FACE__EYE_OUTLINE_LEFT__TOP
)

from .helpers import get_eye_openness


def get_left_eye_openness(landmarks):
    return get_eye_openness(
        landmarks[POINT_FACE__EYE_OUTLINE_LEFT__INNER_2],
        landmarks[POINT_FACE__EYE_OUTLINE_LEFT__OUTER_2],
        landmarks[POINT_FACE__EYE_OUTLINE_LEFT__BOT],
        landmarks[POINT_FACE__EYE_OUTLINE_LEFT__TOP],
    )
