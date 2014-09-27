namespace TCoSServer
{
  namespace Login
  {
    public enum LoginMessageId : ushort
    {
      CONNECT = 0xFFFD,
      DISCONNECT = 0xFFFE,
      C2L_USER_LOGIN = 0,
      L2C_USER_LOGIN_ACK = 1,

      C2L_QUERY_UNIVERSE_LIST = 2,
      L2C_QUERY_UNIVERSE_LIST_ACK = 3,
      C2L_UNIVERSE_SELECTED = 4,
      L2C_UNIVERSE_SELECTED_ACK = 5
    }
  }
}