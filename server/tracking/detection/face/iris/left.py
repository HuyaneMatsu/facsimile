__all__ = ('get_left_iris_rotations', )


from ....points.face import (
    POINT_FACE__EYE_OUTLINE_LEFT__INNER, POINT_FACE__EYE_OUTLINE_LEFT__OUTER,
    POINT_FACE__EYE_OUTLINE_LEFT__BOT_1, POINT_FACE__EYE_OUTLINE_LEFT__TOP_1,
    POINT_FACE__IRIS_LEFT__MID
)

from .helpers import get_iris_rotations


def get_left_iris_rotations(landmarks):
    ratios = get_iris_rotations(
        landmarks[POINT_FACE__IRIS_LEFT__MID],
        landmarks[POINT_FACE__EYE_OUTLINE_LEFT__TOP_1],
        landmarks[POINT_FACE__EYE_OUTLINE_LEFT__BOT_1],
        landmarks[POINT_FACE__EYE_OUTLINE_LEFT__INNER],
        landmarks[POINT_FACE__EYE_OUTLINE_LEFT__OUTER],
    )
    return -ratios[0], ratios[1]

