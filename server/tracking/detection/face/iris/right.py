__all__ = ('get_right_iris_rotations',)

from ....points.face import (
    POINT_FACE__EYE_OUTLINE_RIGHT__INNER, POINT_FACE__EYE_OUTLINE_RIGHT__OUTER,
    POINT_FACE__EYE_OUTLINE_RIGHT__BOT_1, POINT_FACE__EYE_OUTLINE_RIGHT__TOP_1,
    POINT_FACE__IRIS_RIGHT__MID
)

from .helpers import get_iris_rotations


def get_right_iris_rotations(landmarks):
    return get_iris_rotations(
        landmarks[POINT_FACE__IRIS_RIGHT__MID],
        landmarks[POINT_FACE__EYE_OUTLINE_RIGHT__TOP_1],
        landmarks[POINT_FACE__EYE_OUTLINE_RIGHT__BOT_1],
        landmarks[POINT_FACE__EYE_OUTLINE_RIGHT__INNER],
        landmarks[POINT_FACE__EYE_OUTLINE_RIGHT__OUTER],
    )
