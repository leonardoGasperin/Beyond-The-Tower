namespace BTT.aplicacao.contratos {
    public interface IMovimentacaoServico{
        public void Movimentacao(Rigidbody2D rb, float velocidade, float movimentoX);
        public void Pulo(Rigidbody2D rb, transform transform, float forcaDoPulo);
    }
}