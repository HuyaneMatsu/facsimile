__all__ = ('get_left_iris_rotations', )


from ..face_mesh_points import (
    FACE_MESH_POINT__EYE_OUTLINE_LEFT__INNER, FACE_MESH_POINT__EYE_OUTLINE_LEFT__OUTER,
    FACE_MESH_POINT__EYE_OUTLINE_LEFT__BOT_OVER, FACE_MESH_POINT__EYE_OUTLINE_LEFT__TOP_OVER,
    FACE_MESH_POINT__IRIS_LEFT__MID
)

from .helpers import get_iris_rotations


def get_left_iris_rotations(landmarks):
    ratios = get_iris_rotations(
        landmarks[FACE_MESH_POINT__IRIS_LEFT__MID],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__TOP_OVER],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__BOT_OVER],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__INNER],
        landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__OUTER],
    )
    return -ratios[0], ratios[1]

