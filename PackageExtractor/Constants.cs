using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageExtractor
{
  public enum ObjectFlags : int
  {
     RF_HasStack = 0x02000000
    //to be continued
  }

  public enum PropertyFlags : int
  {
    CPF_Edit = 0x01,
    CPF_Const = 0x02,
    CPF_Input = 0x04,
    CPF_ExportObject = 0x08,
    CPF_OptionalParm = 0x10,
    CPF_Net = 0x20,
    CPF_ConstRef = 0x40,
    CPF_Parm = 0x80,
    CPF_OutParm = 0x100,
    CPF_SkipParm = 0x200
    //to be continued
  }

  public enum FunctionFlags : int
  {
    FUNC_Final = 0x01,
    FUNC_Defined = 0x02,
    FUNC_Iterator = 0x04,
    FUNC_Latent = 0x08,
    FUNC_PreOperator = 0x10,
    FUNC_Singular = 0x20,
    FUNC_Net = 0x40,
    FUNC_NetReliable = 0x80
    //To be continued
  }
}
