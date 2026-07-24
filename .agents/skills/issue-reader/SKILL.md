---
name: issue-reader
description: "Retrieve GitHub issue information using the GitHub CLI (gh). USE FOR: viewing a specific issue by number, searching issues by topic or keyword, listing open/closed issues, and summarizing issue details. DO NOT USE FOR: creating issues (use issue-writer), commenting on issues, or closing issues."
compatibility: "Requires GitHub CLI (gh) authenticated to the target repository. Works on Windows (PowerShell), macOS, and Linux (bash/zsh)."
---

# Issue Reader — Retrieve GitHub Issue Information via GitHub CLI

## Trigger On

- user asks to look up, find, or show a GitHub issue
- user provides an issue number and wants its details
- user describes a topic and wants to search for matching issues
- user asks to list open or closed issues
- user wants a summary of what a specific issue is about

## Prerequisites

Verify `gh` is available and authenticated:

```sh
gh auth status
```

If not authenticated, instruct the user to run:

```sh
gh auth login
```

## Determine the Repository

Always use the repository of the current workspace. Detect it automatically from the Git remote **before running any `gh` command** and store it for reuse:

```sh
git remote get-url origin
```

If the command exits with a non-zero code or returns no output, stop and tell the user: "No Git remote named `origin` was found in this workspace. Please run this skill from inside the repository you want to query."

Do **not** ask the user to provide or override the repository. All `gh` commands in this skill must use the detected `owner/repo` via `--repo owner/repo` so they always target the current workspace repository regardless of the shell's working directory.

---

## Workflow

### 1. Identify the retrieval mode

Determine what the user wants based on their input:

| User provides | Mode |
|---------------|------|
| An issue number (e.g., `#42` or `42`) | **View by number** |
| A keyword, topic, or description | **Search by topic** |
| No specific issue — wants an overview | **List issues** |

---

### 2a. View a specific issue by number

Fetch the full details of a single issue:

```sh
gh issue view 42 --repo owner/repo
```

To get machine-readable JSON (useful for further processing):

```sh
gh issue view 42 --repo owner/repo --json number,title,body,state,labels,assignees,milestone,createdAt,updatedAt,url
```

Show the issue in the browser:

```sh
gh issue view 42 --repo owner/repo --web
```

**Output summary to present to the user:**

| Field | Description |
|-------|-------------|
| `number` | Issue number |
| `title` | Issue title |
| `state` | `OPEN` or `CLOSED` |
| `labels` | Assigned labels |
| `assignees` | Assigned users |
| `milestone` | Associated milestone |
| `body` | Full issue description |
| `url` | Direct link |

---

### 2b. Search for issues by topic or keyword

Use `--search` to filter issues by keyword, label, assignee, or state. The search query supports [GitHub search syntax](https://docs.github.com/en/search-github/searching-on-github/searching-issues-and-pull-requests). All commands target the current workspace repository.

**Search by keyword:**

```sh
gh issue list --repo owner/repo --search "<keyword>"
```

**Search open issues only (default):**

```sh
gh issue list --repo owner/repo --state open --search "<keyword>"
```

**Search closed issues:**

```sh
gh issue list --repo owner/repo --state closed --search "<keyword>"
```

**Search by label:**

```sh
gh issue list --repo owner/repo --label "bug" --search "<keyword>"
```

**Limit results (default is 30):**

```sh
gh issue list --repo owner/repo --search "<keyword>" --limit 10
```

**Output as JSON for structured processing:**

```sh
gh issue list --repo owner/repo --search "<keyword>" \
  --json number,title,state,labels,assignees,url
```

---

### 2c. List issues (no specific search term)

All commands target the current workspace repository.

**All open issues:**

```sh
gh issue list --repo owner/repo
```

**All issues (open and closed):**

```sh
gh issue list --repo owner/repo --state all
```

**Issues assigned to a specific user:**

```sh
gh issue list --repo owner/repo --assignee "username"
```

**Issues with a specific label:**

```sh
gh issue list --repo owner/repo --label "enhancement"
```

**Issues for a specific milestone:**

```sh
gh issue list --repo owner/repo --milestone "v1.0"
```

---

### 3. Present results to the user

**For a single issue (view mode):** Present the key fields in a readable summary. Include the direct URL at the end.

**For a list or search result:** Present as a markdown table with columns: `#`, `Title`, `State`, `Labels`, `URL`. Limit to the most relevant results. If the list is long, ask the user if they want to narrow the search.

---

## Useful Field Reference for JSON Output

When using `--json`, available fields include:

| Field | Type | Description |
|-------|------|-------------|
| `number` | int | Issue number |
| `title` | string | Issue title |
| `body` | string | Issue description (markdown) |
| `state` | string | `OPEN` or `CLOSED` |
| `labels` | array | Label names |
| `assignees` | array | GitHub usernames |
| `milestone` | object | Milestone title and due date |
| `author` | object | Issue author login |
| `comments` | array | Issue comments |
| `createdAt` | string | ISO 8601 creation date |
| `updatedAt` | string | ISO 8601 last update date |
| `closedAt` | string | ISO 8601 close date (if closed) |
| `url` | string | HTML URL of the issue |

---

## Error Handling

| Error | Fix |
|-------|-----|
| `gh: command not found` | Install GitHub CLI: https://cli.github.com |
| `You are not logged into any GitHub hosts` | Run `gh auth login` |
| `Could not resolve to an issue` | Verify the issue number exists; run `gh issue list` to confirm |
| `Repository not found` | Ask the user to verify the `owner/repo` slug and re-run with `--repo "<corrected-owner/repo>"` |
| `Permission denied` | Ensure the authenticated user has read access to the repository |
| No results from `--search` | Broaden the query, remove filters, or try `--state all` |
