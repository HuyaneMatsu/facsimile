__all__ = ('get_right_eye_closedness',)

from .helpers import get_eye_closedness

from ..face_mesh_points import (
    FACE_MESH_POINT__EYE_OUTLINE_RIGHT__BOT, FACE_MESH_POINT__EYE_OUTLINE_RIGHT__INNER,
    FACE_MESH_POINT__EYE_OUTLINE_RIGHT__OUTER, FACE_MESH_POINT__EYE_OUTLINE_RIGHT__TOP
)


def get_right_eye_closedness(landmarks):
    return get_eye_closedness(
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_RIGHT__INNER],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_RIGHT__OUTER],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_RIGHT__BOT],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_RIGHT__TOP],
    )

