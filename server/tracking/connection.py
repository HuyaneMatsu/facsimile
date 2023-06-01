__all__ = ('try_connect_socket',)

from time import sleep
from socket import socket as Socket, AF_INET as SOCKET_FAMILY__AF_INET, SOCK_STREAM as SOCKET_TYPE__STREAM

from .constants import ADDRESS, RETRY_AFTER
from .helpers import format_address, log


def try_connect_socket():
    """
    Tries to connect with socket.
    
    Returns
    -------
    socket : `socket.socket`
    """
    log(f'CONNECTION | Trying to connect to: {format_address(ADDRESS)}')
    
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
    
    
    log(f'CONNECTION | Connected to: {format_address(ADDRESS)}')
    return socket
