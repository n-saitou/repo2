using DDD.Domain.Exceptions;

namespace DDD.Domain.Helpers
{
    public static class Guard
    {
        public static void IsNull(object o, string message)
        {
            if (o == null)
            {
                throw new InputException(message);
            }
        }

        public static float IsFloat(string text, string message)
        {
            if (float.TryParse(text, out var floatValue))
            {
                return floatValue;
            }
            else
            {
                throw new InputException(message);
            }
        }

    }
}
