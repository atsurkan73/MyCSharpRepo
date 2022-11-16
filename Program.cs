/* 
Create a program to ‘vote’ for anything. Via the console interface users will create a ‘vote topic’ with options.
Voters will vote via console interface as well. Users can see voting results via console interface.
*/

using CollectionLesson;


VoteSystem voteSystem = new VoteSystem();
UserData userData = new UserData();
Vote vote = new Vote();

List<string> userList;
Dictionary<int, string> voteOptions;





userList = userData.CreateUser();
voteOptions = voteSystem.DefineVoteSystem();
vote.SelectUser(userList);
vote.VotingResult(voteOptions);

