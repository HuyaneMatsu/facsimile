__all__ = ()

from .constants import EXPRESSION_MULTIPLIER, SADNESS_REDUCTION, EXPRESSION_MIN, EXPRESSION_MAX, SURPRISE_REDUCTION


def normalize_expression(expression):
    if expression < EXPRESSION_MIN:
        expression = EXPRESSION_MIN
    else:
        expression *= EXPRESSION_MULTIPLIER
        if expression > EXPRESSION_MAX:
            expression = EXPRESSION_MAX
    
    return expression


def normalize_expressions(expressions):
    neutral, happiness, sadness, surprise, fear, disgust, anger = expressions
    
    if (neutral > 0.0):
        sadness -= neutral * SADNESS_REDUCTION
        surprise -= neutral * SURPRISE_REDUCTION
        
    
    happiness = normalize_expression(happiness)
    sadness = normalize_expression(sadness)
    surprise = normalize_expression(surprise)
    fear = normalize_expression(fear)
    disgust = normalize_expression(disgust)
    anger = normalize_expression(anger)
    
    return happiness, sadness, surprise, fear, disgust, anger
