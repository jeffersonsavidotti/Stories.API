namespace Stories.API.Applications.ViewModels
{
    public class VoteViewModel
    {
        public int Id { get; set; }
        public int IdStory { get; set; }
        public int IdUser { get; set; }
        public bool VoteValue { get; set; }

        // Adicione outras propriedades conforme necessário, por exemplo, para mostrar informações relacionadas
    }
}
