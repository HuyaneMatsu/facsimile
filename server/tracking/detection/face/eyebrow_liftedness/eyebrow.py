__all__ = ('get_eyebrow_liftedness', )

from ....helpers import get_point_average_3d, get_point_difference_3d
from ....points.face import (
    POINT_FACE__EYEBROW_RIGHT__TOP, POINT_FACE__FOREHEAD__LEFT_1,
    POINT_FACE__EYEBROW_RIGHT__BOT, POINT_FACE__FOREHEAD__RIGHT_1,
    POINT_FACE__EYEBROW_LEFT__BOT, POINT_FACE__EYE_OUTLINE_RIGHT__INNER,
    POINT_FACE__EYEBROW_LEFT__TOP, POINT_FACE__EYE_OUTLINE_LEFT__INNER
)

from .constants import (
    EYEBROW_LIFTEDNESS_REDUCTION, EYEBROW_LIFTEDNESS_MULTIPLIER, EYEBROW_LIFTEDNESS_MIN, EYEBROW_LIFTEDNESS_MAX
)


def get_eyebrow_liftedness(landmarks):
    eyebrow_liftedness_left = get_side_eyebrow_lift_ratio(
        landmarks[POINT_FACE__EYE_OUTLINE_LEFT__INNER],
        landmarks[POINT_FACE__EYEBROW_LEFT__TOP],
        landmarks[POINT_FACE__EYEBROW_LEFT__BOT],
        landmarks[POINT_FACE__FOREHEAD__LEFT_1],
    )
    eyebrow_liftedness_right = get_side_eyebrow_lift_ratio(
        landmarks[POINT_FACE__EYE_OUTLINE_RIGHT__INNER],
        landmarks[POINT_FACE__EYEBROW_RIGHT__TOP],
        landmarks[POINT_FACE__EYEBROW_RIGHT__BOT],
        landmarks[POINT_FACE__FOREHEAD__RIGHT_1],
    )
    
    if (eyebrow_liftedness_left > eyebrow_liftedness_right):
        eyebrow_liftedness = eyebrow_liftedness_left
    else:
        eyebrow_liftedness = eyebrow_liftedness_right
    
    
    eyebrow_liftedness = (eyebrow_liftedness - EYEBROW_LIFTEDNESS_REDUCTION) * EYEBROW_LIFTEDNESS_MULTIPLIER
    
    if eyebrow_liftedness < EYEBROW_LIFTEDNESS_MIN:
        eyebrow_liftedness = EYEBROW_LIFTEDNESS_MIN
    elif eyebrow_liftedness > EYEBROW_LIFTEDNESS_MAX:
        eyebrow_liftedness = EYEBROW_LIFTEDNESS_MAX
    
    return eyebrow_liftedness


def get_side_eyebrow_lift_ratio(forehead_point, eyebrow_top_point, eyebrow_bot_point, eye_point):
    return get_point_difference_3d(
        get_point_average_3d(eyebrow_top_point, eyebrow_bot_point),
        forehead_point,
    ) / get_point_difference_3d(
        forehead_point,
        eye_point,
    )
