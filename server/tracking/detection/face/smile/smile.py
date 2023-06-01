__all__ = ('get_smile_ratio',)

from ....helpers import get_point_difference_3d
from ....points.face import (
    POINT_FACE__CHEEKS_SMILE_LEFT__TOP_3, POINT_FACE__CHEEKS_SMILE_RIGHT__TOP_3,
    POINT_FACE__FACE_LEFT_MOST, POINT_FACE__FACE_RIGHT_MOST, POINT_FACE__MOUTH_LEFT_MOST,
    POINT_FACE__MOUTH_RIGHT_MOST, POINT_FACE__CHEEKS_SMILE_RIGHT__MID_3,
    POINT_FACE__CHEEKS_SMILE_LEFT__MID_3
)

from .constants import SMILE_REDUCTION, SMILE_MAX, SMILE_MIN, SMILE_MULTIPLIER


def get_smile_ratio(landmarks):
    head_width = get_point_difference_3d(
        landmarks[POINT_FACE__FACE_LEFT_MOST],
        landmarks[POINT_FACE__FACE_RIGHT_MOST],
    )
    
    left_smile_ratio = get_side_smile_ratio(
        landmarks[POINT_FACE__MOUTH_LEFT_MOST],
        landmarks[POINT_FACE__CHEEKS_SMILE_LEFT__TOP_3],
        landmarks[POINT_FACE__CHEEKS_SMILE_LEFT__MID_3],
        head_width,
    )
    
    
    right_smile_ratio = get_side_smile_ratio(
        landmarks[POINT_FACE__MOUTH_RIGHT_MOST],
        landmarks[POINT_FACE__CHEEKS_SMILE_RIGHT__TOP_3],
        landmarks[POINT_FACE__CHEEKS_SMILE_RIGHT__MID_3],
        head_width,
    )
    
    if (left_smile_ratio > right_smile_ratio):
        smile_ratio = left_smile_ratio
    else:
        smile_ratio = right_smile_ratio
    
    smile_ratio = (smile_ratio - SMILE_REDUCTION) * SMILE_MULTIPLIER
    
    if smile_ratio < SMILE_MIN:
        smile_ratio = SMILE_MIN
    elif smile_ratio > SMILE_MAX:
        smile_ratio = SMILE_MAX
    
    return smile_ratio


def get_side_smile_ratio(mouth_point, cheek_top_point, cheek_mid_point, head_width):
    return head_width / (
        get_point_difference_3d(mouth_point, cheek_top_point) +
        get_point_difference_3d(mouth_point, cheek_mid_point)
    )
