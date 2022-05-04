__all__ = ('get_left_eye_openness',)

from .helpers import get_eye_openness

from ..face_mesh_points import (
    FACE_MESH_POINT__EYE_OUTLINE_LEFT__BOT, FACE_MESH_POINT__EYE_OUTLINE_LEFT__INNER_2,
    FACE_MESH_POINT__EYE_OUTLINE_LEFT__OUTER_2, FACE_MESH_POINT__EYE_OUTLINE_LEFT__TOP
)


def get_left_eye_openness(landmarks):
    return get_eye_openness(
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__INNER_2],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__OUTER_2],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__BOT],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__TOP],
    )
