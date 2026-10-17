using System.Collections.Generic;

public static class UpgradeManager {
    private static Dictionary<UpgradeType, int> nivelUpgrade = new Dictionary<UpgradeType, int>();
    //UpgradeType: chave
    //int: valor

    //metodo para pegar o nivel atual do upgrade
    public static int GetNivel(UpgradeType tipo) {
        if(nivelUpgrade.ContainsKey(tipo)) {
            return nivelUpgrade[tipo];
        }

        return 0;
    }

    //metodo para setar novo nivel no upgrade
    public static void SetNivel(UpgradeType tipo, int nivelMaximo) {
        if(!nivelUpgrade.ContainsKey(tipo)) {
            nivelUpgrade[tipo] = 0;
        }

        if(nivelUpgrade[tipo] < nivelMaximo)
            nivelUpgrade[tipo]++;
    }
}