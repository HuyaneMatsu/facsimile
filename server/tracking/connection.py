__all__ = ('try_connect_socket',)

from time import sleep
from socket import socket as Socket, AF_INET as SOCKET_FAMILY__AF_INET, SOCK_STREAM as SOCKET_TYPE__STREAM

from .constants import ADDRESS, RETRY_AFTER


def try_connect_socket():
    socket = Socket(SOCKET_FAMILY__AF_INET, SOCKET_TYPE__STREAM)
    
    while True:
        try:
            socket.connect(ADDRESS)
        except ConnectionRefusedError:
            pass
        else:
            break
        
        sleep(RETRY_AFTER)
        continue
    
    return socket
