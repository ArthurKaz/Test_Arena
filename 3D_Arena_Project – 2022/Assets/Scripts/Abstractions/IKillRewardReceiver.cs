namespace Test_Project.Abstractions
{
    public interface IKillRewardReceiver
    {
        public void ReceiveKillReward(float energy);
        public void ReceiveRewardForReboundKill();
    }
}