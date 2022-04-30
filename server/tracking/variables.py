__all__ = ()

from sys import argv as COMMAND_LINE_PARAMETERS


SHOULD_DRAW = ('--draw' in COMMAND_LINE_PARAMETERS)
SHOULD_CONNECT = ('--no-connect' not in COMMAND_LINE_PARAMETERS)
