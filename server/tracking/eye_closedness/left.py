__all__ = ('get_left_eye_closedness',)

from .helpers import get_eye_closedness

from ..face_mesh_points import (
    FACE_MESH_POINT__EYE_OUTLINE_LEFT__BOT, FACE_MESH_POINT__EYE_OUTLINE_LEFT__INNER,
    FACE_MESH_POINT__EYE_OUTLINE_LEFT__OUTER, FACE_MESH_POINT__EYE_OUTLINE_LEFT__TOP
)


def get_left_eye_closedness(landmarks):
    return get_eye_closedness(
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__INNER],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__OUTER],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__BOT],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__TOP],
    )
