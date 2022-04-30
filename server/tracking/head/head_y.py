__all__ = ('get_head_y_rotation',)

from math import sqrt

from ..face_mesh_points import FACE_MESH_POINT__FOREHEAD, FACE_MESH_POINT__NOSE_RADIX, FACE_MESH_POINT__NOSE_TIP


def get_head_y_rotation(landmarks):
    mid = landmarks[FACE_MESH_POINT__NOSE_RADIX]
    top = landmarks[FACE_MESH_POINT__NOSE_TIP]
    if top.y <= mid.y:
        return 60.0
    
    bot = landmarks[FACE_MESH_POINT__FOREHEAD]
    
    difference_mid_bot = sqrt((top.y - mid.y) ** 2 + (top.x - mid.x) ** 2)
    difference_top_bot = sqrt((mid.y - bot.y) ** 2 + (mid.x - bot.x) ** 2)
    
    rotate_y =  500.0 * ((difference_mid_bot / (difference_mid_bot + difference_top_bot)) - 0.5)
    
    if rotate_y < -60.0:
        rotate_y = -60.0
    
    elif rotate_y > 30.0:
        rotate_y = +30.0
    
    return rotate_y
