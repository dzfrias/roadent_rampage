public interface IMover
{
    float MaxSpeed { get; set; }

    void Turn(float turnInput);
    void Accelerate(float speed);
    bool IsGrounded();
}
