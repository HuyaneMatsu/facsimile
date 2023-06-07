__all__ = ('get_shoulder_z_rotation',)

from math import atan2, tau

from ....points.body import POINT_BODY__SHOULDER__LEFT, POINT_BODY__SHOULDER__RIGHT


def get_shoulder_z_rotation(landmarks):
    right = landmarks[POINT_BODY__SHOULDER__RIGHT]
    left = landmarks[POINT_BODY__SHOULDER__LEFT]
    
    return -((atan2(right.x - left.x, right.y - left.y) * 360.0 / tau) + 90.0)
