public interface IObjectPool {
  void Initialize();
  bool IsReady { get; }
}
