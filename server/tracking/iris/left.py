__all__ = ('get_left_iris_rotations', )


from ..face_mesh_points import (
    FACE_MESH_POINT__IRIS_LEFT__INNER, FACE_MESH_POINT__IRIS_LEFT__OUTER, FACE_MESH_POINT__IRIS_LEFT__BOT,
    FACE_MESH_POINT__IRIS_LEFT__TOP, FACE_MESH_POINT__EYE_OUTLINE_LEFT__INNER,
    FACE_MESH_POINT__EYE_OUTLINE_LEFT__OUTER, FACE_MESH_POINT__EYE_OUTLINE_LEFT__BOT_OVER,
    FACE_MESH_POINT__EYE_OUTLINE_LEFT__TOP_OVER
)

from .helpers import get_iris_rotations, scale_iris_rotations


def get_left_iris_rotations(landmarks):
    iris__x__high = -landmarks[FACE_MESH_POINT__IRIS_LEFT__INNER].x
    iris__x__low = -landmarks[FACE_MESH_POINT__IRIS_LEFT__OUTER].x
    
    iris__y__high = landmarks[FACE_MESH_POINT__IRIS_LEFT__BOT].y
    iris__y__low = landmarks[FACE_MESH_POINT__IRIS_LEFT__TOP].y
    
    eye_outline__x__high = -landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__INNER].x
    eye_outline__x__low = -landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__OUTER].x
    
    eye_outline__y__high =  landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__BOT_OVER].y
    eye_outline__y__low =  landmarks[FACE_MESH_POINT__EYE_OUTLINE_LEFT__TOP_OVER].y
    
    ratios = get_iris_rotations(
        iris__x__high, iris__x__low, iris__y__high, iris__y__low, eye_outline__x__high, eye_outline__x__low,
        eye_outline__y__high, eye_outline__y__low
    )
    
    if (ratios is not None):
        ratios = scale_iris_rotations(-ratios[0], ratios[1])
    
    return ratios
