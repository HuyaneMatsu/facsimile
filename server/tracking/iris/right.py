__all__ = ('get_right_iris_rotations',)

from ..face_mesh_points import (
    FACE_MESH_POINT__EYE_OUTLINE_RIGHT__INNER, FACE_MESH_POINT__EYE_OUTLINE_RIGHT__OUTER,
    FACE_MESH_POINT__EYE_OUTLINE_RIGHT__BOT_OVER, FACE_MESH_POINT__EYE_OUTLINE_RIGHT__TOP_OVER,
    FACE_MESH_POINT__IRIS_RIGHT__MID
)

from .helpers import get_iris_rotations


def get_right_iris_rotations(landmarks):
    return get_iris_rotations(
        landmarks[FACE_MESH_POINT__IRIS_RIGHT__MID],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_RIGHT__TOP_OVER],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_RIGHT__BOT_OVER],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_RIGHT__INNER],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_RIGHT__OUTER],
    )
