__all__ = ('get_eyebrow_liftedness', )

from ..face_mesh_points import (
    FACE_MESH_POINT__EYEBROW_RIGHT__INNER_TOP, FACE_MESH_POINT__FOREHEAD__LEFT_1,
    FACE_MESH_POINT__EYEBROW_RIGHT__INNER_BOT, FACE_MESH_POINT__FOREHEAD__RIGHT_1,
    FACE_MESH_POINT__EYEBROW_LEFT__INNER_BOT, FACE_MESH_POINT__EYE_OUTLINE_RIGHT__INNER,
    FACE_MESH_POINT__EYEBROW_LEFT__INNER_TOP, FACE_MESH_POINT__EYE_OUTLINE_LEFT__INNER

)
from ..helpers import point_average_3d, get_point_difference_3d

from .constants import (
    EYEBROW_LIFTEDNESS_REDUCTION, EYEBROW_LIFTEDNESS_MULTIPLIER, EYEBROW_LIFTEDNESS_MIN, EYEBROW_LIFTEDNESS_MAX
)

def get_eyebrow_liftedness(landmarks):
    eyebrow_liftedness_left = get_side_eyebrow_lift_ratio(
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__INNER],
        landmarks[FACE_MESH_POINT__EYEBROW_LEFT__INNER_TOP],
        landmarks[FACE_MESH_POINT__EYEBROW_LEFT__INNER_BOT],
        landmarks[FACE_MESH_POINT__FOREHEAD__LEFT_1],
    )
    eyebrow_liftedness_right = get_side_eyebrow_lift_ratio(
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_RIGHT__INNER],
        landmarks[FACE_MESH_POINT__EYEBROW_RIGHT__INNER_TOP],
        landmarks[FACE_MESH_POINT__EYEBROW_RIGHT__INNER_BOT],
        landmarks[FACE_MESH_POINT__FOREHEAD__RIGHT_1],
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
        point_average_3d(eyebrow_top_point, eyebrow_bot_point),
        forehead_point,
    ) / get_point_difference_3d(
        forehead_point,
        eye_point,
    )
