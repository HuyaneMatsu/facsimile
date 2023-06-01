__all__ = ()

from sys import argv as COMMAND_LINE_PARAMETERS


def get_named_parameter(name):
    """
    Gets a named parameter's value.
    
    Parameters
    ----------
    name : `str`
        The parameter's name.
    
    Returns
    -------
    value : `None`, `str`
    """
    command_line_parameter_count = len(COMMAND_LINE_PARAMETERS)
    
    try:
        index = COMMAND_LINE_PARAMETERS.index(name)
    except ValueError:
        return None
    
    index += 1
    
    if index >= command_line_parameter_count:
        return None
    
    value = COMMAND_LINE_PARAMETERS[index]
    if value.startswith('-'):
        return None
    
    return value


SHOULD_DRAW = ('--draw' in COMMAND_LINE_PARAMETERS)
SHOULD_CONNECT = ('--no-connect' not in COMMAND_LINE_PARAMETERS)

ALLOW_FACE = ('--no-face' not in COMMAND_LINE_PARAMETERS)
ALLOW_EXPRESSIONS = ('--no-expressions' not in COMMAND_LINE_PARAMETERS)
ALLOW_BODY = ('--no-body' not in COMMAND_LINE_PARAMETERS)

CONNECT_URL = get_named_parameter('--to')
