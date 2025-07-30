namespace backend.Shared.Constants
{
    public static class ErrorMessages
    {
        public const string PularEstacao = "Nao e permitido pular estacoes";
        public const string MesmaEstacao = "A peca ja esta na estacao informada";
        public const string RetrocederEstacao = "Nao e permitido retroceder estacoes";
        public const string PecaFinalizada = "Esta Peca ja foi finalizada";
        public const string ProximaEstacaoNaoEncontrada = "Proxima Estacao nao encontrada";

        public const string OrdemExiste = "Esse valor de Ordem ja esta em uso";
        public const string OrdemMenorIgualZero = "O valor da Ordem precisa ser superior a Zero";
    }
}