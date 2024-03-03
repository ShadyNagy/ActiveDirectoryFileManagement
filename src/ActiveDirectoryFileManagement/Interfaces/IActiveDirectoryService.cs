namespace ActiveDirectoryFileManagement.Interfaces;
public interface IActiveDirectoryService
{
	TResult ImpersonateUserAndRunAction<TResult>(Func<TResult> action);
	void ImpersonateUserAndRunAction(Action action);
}
