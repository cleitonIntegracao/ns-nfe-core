using ns_nfe_core.src.emissao.xsd;

namespace ns_nfe_core_integration.nfe.constructor
{
    internal class infNFe
    {
        public static TNFeInfNFe infNFeConstructor(object[] conteudo)
        {
            TNFeInfNFe infNFe = new TNFeInfNFe();

            foreach (object obj in conteudo)
            {
                var grupo = obj.GetType();
            }

            return infNFe;
        }
    }
}
