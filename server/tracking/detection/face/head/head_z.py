__all__ = ('get_head_z_rotation',)

from math import atan2, tau

from ....points.face import POINT_FACE__FOREHEAD__LEFT, POINT_FACE__FOREHEAD__RIGHT


def get_head_z_rotation(landmarks):
    right = landmarks[POINT_FACE__FOREHEAD__RIGHT]
    left = landmarks[POINT_FACE__FOREHEAD__LEFT]
    
    return atan2(left.y - right.y, left.x - right.x) * 180.0 / tau
