__all__ = ('get_head_y_rotation',)

from math import atan2, tau

from ..face_mesh_points import FACE_MESH_POINT__FOREHEAD, FACE_MESH_POINT__NOSE_TIP


def get_head_y_rotation(landmarks):
    right = landmarks[FACE_MESH_POINT__FOREHEAD]
    left = landmarks[FACE_MESH_POINT__NOSE_TIP]
    
    return atan2(left.z - right.z, left.y - right.y) * 180.0 / tau
