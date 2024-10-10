using System.Text.Json.Serialization;

namespace Phahtshiu.Functions.Models;

public class GitHubWebhookPayload 
{
    [JsonPropertyName("ref")]
    public string Ref { get; set; }
    
    [JsonPropertyName("before")]
    public string Before { get; set; }
    
    [JsonPropertyName("after")]
    public string After { get; set; }
    
    [JsonPropertyName("repository")]
    public GitHubRepository Repository { get; set; }
    
    [JsonPropertyName("pusher")]
    public GithubPusher Pusher { get; set; }
    
    [JsonPropertyName("sender")]
    public GithubSender Sender { get; set; }
    
    [JsonPropertyName("created")]
    public bool Created { get; set; }
    
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }
    
    [JsonPropertyName("forced")]
    public bool Forced { get; set; }
    
    [JsonPropertyName("base_ref")]
    public string BaseRef { get; set; }
    
    [JsonPropertyName("compare")]
    public string Compare { get; set; }
    
    [JsonPropertyName("commits")]
    public List<GithubCommit> Commits { get; set; }
    
    [JsonPropertyName("head_commit")]
    public GithubCommit HeadCommit { get; set; }
}

public class GitHubRepository 
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("node_id")]
    public string NodeId { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("full_name")]
    public string FullName { get; set; }
    
    [JsonPropertyName("private")]
    public bool Private { get; set; }
    
    [JsonPropertyName("owner")]
    public GithubRepositoryOwner GithubRepositoryOwner { get; set; }
    
    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("fork")]
    public bool Fork { get; set; }
    
    [JsonPropertyName("url")]
    public string Url { get; set; }
    
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }
    
    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; set; }
    
    [JsonPropertyName("pushed_at")]
    public long PushedAt { get; set; }
    
    [JsonPropertyName("git_url")]
    public string GitUrl { get; set; }
    
    [JsonPropertyName("ssh_url")]
    public string SshUrl { get; set; }
    
    [JsonPropertyName("clone_url")]
    public string CloneUrl { get; set; }
    
    [JsonPropertyName("svn_url")]
    public string SvnUrl { get; set; }
    
    [JsonPropertyName("size")]
    public int Size { get; set; }
    
    [JsonPropertyName("stargazers_count")]
    public int StargazersCount { get; set; }
    
    [JsonPropertyName("watchers_count")]
    public int WatchersCount { get; set; }
    
    [JsonPropertyName("language")]
    public string Language { get; set; }
    
    [JsonPropertyName("has_issues")]
    public bool HasIssues { get; set; }
    
    [JsonPropertyName("has_projects")]
    public bool HasProjects { get; set; }
    
    [JsonPropertyName("has_downloads")]
    public bool HasDownloads { get; set; }
    
    [JsonPropertyName("has_wiki")]
    public bool HasWiki { get; set; }
    
    [JsonPropertyName("has_pages")]
    public bool HasPages { get; set; }
    
    [JsonPropertyName("has_discussions")]
    public bool HasDiscussions { get; set; }
    
    [JsonPropertyName("forks_count")]
    public int ForksCount { get; set; }
    
    [JsonPropertyName("archived")]
    public bool Archived { get; set; }
    
    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }
    
    [JsonPropertyName("open_issues_count")]
    public int OpenIssuesCount { get; set; }
    
    [JsonPropertyName("allow_forking")]
    public bool AllowForking { get; set; }
    
    [JsonPropertyName("is_template")]
    public bool IsTemplate { get; set; }
    
    [JsonPropertyName("web_commit_signoff_required")]
    public bool WebCommitSignoffRequired { get; set; }
    
    [JsonPropertyName("visibility")]
    public string Visibility { get; set; }
    
    [JsonPropertyName("forks")]
    public int Forks { get; set; }
    
    [JsonPropertyName("open_issues")]
    public int OpenIssues { get; set; }
    
    [JsonPropertyName("watchers")]
    public int Watchers { get; set; }
    
    [JsonPropertyName("default_branch")]
    public string DefaultBranch { get; set; }
    
    [JsonPropertyName("stargazers")]
    public int Stargazers { get; set; }
    
    [JsonPropertyName("master_branch")]
    public string MasterBranch { get; set; }
}

public class GithubRepositoryOwner 
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("login")]
    public string Login { get; set; }
    
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("node_id")]
    public string NodeId { get; set; }
    
    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; }
    
    [JsonPropertyName("gravatar_id")]
    public string GravatarId { get; set; }
    
    [JsonPropertyName("url")]
    public string Url { get; set; }
    
    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }
    
    [JsonPropertyName("followers_url")]
    public string FollowersUrl { get; set; }
    
    [JsonPropertyName("following_url")]
    public string FollowingUrl { get; set; }
    
    [JsonPropertyName("gists_url")]
    public string GistsUrl { get; set; }
    
    [JsonPropertyName("starred_url")]
    public string StarredUrl { get; set; }
    
    [JsonPropertyName("subscriptions_url")]
    public string SubscriptionsUrl { get; set; }
    
    [JsonPropertyName("organizations_url")]
    public string OrganizationsUrl { get; set; }
    
    [JsonPropertyName("repos_url")]
    public string ReposUrl { get; set; }
    
    [JsonPropertyName("events_url")]
    public string EventsUrl { get; set; }
    
    [JsonPropertyName("received_events_url")]
    public string ReceivedEventsUrl { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("site_admin")]
    public bool SiteAdmin { get; set; }
}

public class GithubPusher 
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
}

public class GithubSender 
{
    [JsonPropertyName("login")]
    public string Login { get; set; }
    
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("node_id")]
    public string NodeId { get; set; }
    
    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; }
    
    [JsonPropertyName("gravatar_id")]
    public string GravatarId { get; set; }
    
    [JsonPropertyName("url")]
    public string Url { get; set; }
    
    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }
    
    [JsonPropertyName("followers_url")]
    public string FollowersUrl { get; set; }
    
    [JsonPropertyName("following_url")]
    public string FollowingUrl { get; set; }
    
    [JsonPropertyName("gists_url")]
    public string GistsUrl { get; set; }
    
    [JsonPropertyName("starred_url")]
    public string StarredUrl { get; set; }
    
    [JsonPropertyName("subscriptions_url")]
    public string SubscriptionsUrl { get; set; }
    
    [JsonPropertyName("organizations_url")]
    public string OrganizationsUrl { get; set; }
    
    [JsonPropertyName("repos_url")]
    public string ReposUrl { get; set; }
    
    [JsonPropertyName("events_url")]
    public string EventsUrl { get; set; }
    
    [JsonPropertyName("received_events_url")]
    public string ReceivedEventsUrl { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("site_admin")]
    public bool SiteAdmin { get; set; }
}

public class GithubCommit 
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("tree_id")]
    public string TreeId { get; set; }
    
    [JsonPropertyName("distinct")]
    public bool Distinct { get; set; }
    
    [JsonPropertyName("message")]
    public string Message { get; set; }
    
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; }
    
    [JsonPropertyName("url")]
    public string Url { get; set; }
    
    [JsonPropertyName("author")]
    public GithubCommitAuthor Author { get; set; }
    
    [JsonPropertyName("committer")]
    public GithubCommitter Committer { get; set; }
    
    [JsonPropertyName("added")]
    public List<string> Added { get; set; }
    
    [JsonPropertyName("removed")]
    public List<string> Removed { get; set; }
    
    [JsonPropertyName("modified")]
    public List<string> Modified { get; set; }
}
public class GithubCommitAuthor 
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("username")]
    public string Username { get; set; }
}

public class GithubCommitter 
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("username")]
    public string Username { get; set; }
}