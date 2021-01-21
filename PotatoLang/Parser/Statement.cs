using System;

namespace PotatoLang.Parser
{
    public struct Statement
    {
        public readonly StatementType StatementType;
        public readonly object[] Operands;
        
        public Statement(StatementType type, params object[] operands)
        {
            StatementType = type;
            Operands = operands;
        }

        public override string ToString()
        {
            return $"{StatementType} % {String.Join(" | ", Operands)}";
        }
    }
}