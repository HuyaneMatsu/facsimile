__all__ = ('get_smile_rate',)

from ..face_mesh_points import (
    FACE_MESH_POINT__CHEEKS_SMILE_LEFT__TOP_3, FACE_MESH_POINT__CHEEKS_SMILE_RIGHT__TOP_3,
    FACE_MESH_POINT__FACE_LEFT_MOST, FACE_MESH_POINT__FACE_RIGHT_MOST, FACE_MESH_POINT__MOUTH_LEFT_MOST,
    FACE_MESH_POINT__MOUTH_RIGHT_MOST, FACE_MESH_POINT__CHEEKS_SMILE_RIGHT__MID_3,
    FACE_MESH_POINT__CHEEKS_SMILE_LEFT__MID_3
)
from ..helpers import get_point_difference_3d

from .constants import SMILE_REDUCTION, MAX_SMILE, MIN_SMILE, SMILE_MULTIPLIER


def get_smile_rate(landmarks):
    head_width = get_point_difference_3d(
        landmarks[FACE_MESH_POINT__FACE_LEFT_MOST],
        landmarks[FACE_MESH_POINT__FACE_RIGHT_MOST],
    )
    
    left_smile_rate = get_side_smile_rate(
        landmarks[FACE_MESH_POINT__MOUTH_LEFT_MOST],
        landmarks[FACE_MESH_POINT__CHEEKS_SMILE_LEFT__TOP_3],
        landmarks[FACE_MESH_POINT__CHEEKS_SMILE_LEFT__MID_3],
        head_width,
    )
    
    
    right_smile_rate = get_side_smile_rate(
        landmarks[FACE_MESH_POINT__MOUTH_RIGHT_MOST],
        landmarks[FACE_MESH_POINT__CHEEKS_SMILE_RIGHT__TOP_3],
        landmarks[FACE_MESH_POINT__CHEEKS_SMILE_RIGHT__MID_3],
        head_width,
    )
    
    if (left_smile_rate > right_smile_rate):
        smile_rate = left_smile_rate
    else:
        smile_rate = right_smile_rate
    
    smile_rate = (smile_rate - SMILE_REDUCTION) * SMILE_MULTIPLIER
    
    if smile_rate < MIN_SMILE:
        smile_rate = MIN_SMILE
    elif smile_rate > MAX_SMILE:
        smile_rate = MAX_SMILE
    
    return smile_rate


def get_side_smile_rate(mouth_point, cheek_top_point, cheek_mid_point, head_width):
    return head_width / (
        get_point_difference_3d(mouth_point, cheek_top_point) +
        get_point_difference_3d(mouth_point, cheek_mid_point)
    )
