__all__ = ('run',)

from time import perf_counter

from .connection import socket_disconnected, try_connect_socket
from .constants import PACKET_TYPE_NONE
from .landmarks_iterator import iter_landmarks
from .variables import SHOULD_CONNECT
from .steppers import BodyDataStepper, ExpressionDataStepper, FaceDataStepper


def run():
    """
    Runs the connect - detect - send loop.
    """
    time = perf_counter()
    
    body_data_stepper = BodyDataStepper(time)
    expression_data_stepper = ExpressionDataStepper(time)
    face_data_stepper = FaceDataStepper(time)
    
    while True:
        if SHOULD_CONNECT:
            socket = try_connect_socket()
        else:
            socket = None
        
        for landmarks in iter_landmarks():
            time = perf_counter()
            
            body_stepped = body_data_stepper.step(time, landmarks)
            if body_stepped and (socket is not None):
                try:
                    socket.send(body_data_stepper.get_data())
                except (BrokenPipeError, ConnectionResetError):
                    break
            
            face_stepped = face_data_stepper.step(time, landmarks)
            if face_stepped and (socket is not None):
                try:
                    socket.send(face_data_stepper.get_data())
                except (BrokenPipeError, ConnectionResetError):
                    break
            
            expression_stepped = expression_data_stepper.step(time, landmarks)
            if expression_stepped and (socket is not None):
                try:
                    socket.send(expression_data_stepper.get_data())
                except (BrokenPipeError, ConnectionResetError):
                    break
            
            # If we did nto step anything send empty packet. This can be used to detect if the ui was stopped.
            if (not (body_stepped or face_stepped or expression_stepped)) and (socket is not None):
                try:
                    socket.send(str(PACKET_TYPE_NONE).encode())
                except (BrokenPipeError, ConnectionResetError):
                    break
        
        else:
            break
        
        if (socket is not None):
            socket_disconnected()
            socket = None
