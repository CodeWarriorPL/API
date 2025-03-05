namespace API.Dto
{
    public class TrainingWithSetsDto
    {
        public string Name { get; set; }
        public DateTime TrainingDate { get; set; }
        public List<SetDto> Sets { get; set; }


    }
}

