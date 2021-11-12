using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ns_nfe_core.src.emissao.xsd;

namespace ns_nfe_core_integration.nfe.constructor
{
    internal class NFe
    {
        public static TNFe nfeConstructor(TNFeInfNFe infNFe) {
            TNFe NFe = new TNFe();
            NFe.infNFe = infNFe;
            return NFe;
        }
    }
}
