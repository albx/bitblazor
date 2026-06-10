---
name: issue-writer
description: "Create GitHub issues using the GitHub CLI (gh). USE FOR: opening a new issue, filing a bug report, requesting a feature, creating a task with labels, assignees, and milestones. DO NOT USE FOR: listing issues (use gh issue list), commenting on PRs, or pushing code changes."
compatibility: "Requires GitHub CLI (gh) authenticated to the target repository. Works on Windows (PowerShell), macOS, and Linux (bash/zsh)."
---

# Issue Writer — Create GitHub Issues via GitHub CLI

## Trigger On

- user wants to create, open, or file a GitHub issue
- user asks to report a bug, request a feature, or log a task
- user provides a title, description, or label for an issue to be opened
- user asks to create an issue in the current repository

## Prerequisites

Before creating issues, verify `gh` is available and authenticated:

```sh
gh auth status
```

If not authenticated, instruct the user to run:

```sh
gh auth login
```

## Issue Templates

This repository defines two issue templates in `.github/ISSUE_TEMPLATE/`. Select one when the user's intent matches a template; use a **blank issue** only when it fits neither.

### `bug_report.md` — Bug Report

Use when the user reports something that is not working as expected.

- **Title prefix**: `[BUG]`
- **Suggested label**: `bug` — verify it exists first with `gh label list`
- **Required sections**:
  - `### Describe the bug` — concise description of what is wrong
  - `### To Reproduce` — numbered steps to reproduce
  - `### Expected behavior` — what should have happened
  - `### Screenshots` *(optional)*
  - `### Desktop` — OS, Browser, Version
  - `### Smartphone` *(optional)* — Device, OS, Browser, Version
  - `### Additional context` *(optional)*

### `new-feature.md` — New Feature

Use when the user requests a new feature or a new component.

- **Title prefix**: `[FEATURE]`
- **Suggested label**: `enhancement` — verify it exists first with `gh label list`
- **Required sections**:
  - Brief description of the feature
  - `### Requirements` — bullet list of what needs to be implemented
  - `### Acceptance Criteria` — definition of done (tests, docs, …)
  - `### References` — links to external resources

---

## Workflow

### 1. Identify the template

Ask the user (or infer from context) which type of issue this is:

| User intent | Template to use |
|-------------|----------------|
| Something is broken / not working | `bug_report.md` |
| New feature / new component / improvement | `new-feature.md` |
| Task, question, chore, or anything else | Blank issue (no template) |

### 2. Gather information

Collect the fields required by the chosen template (ask only for missing ones):

**Bug report fields:**

| Field | Required | Notes |
|-------|----------|-------|
| Title | Yes | Prefix with `[BUG]` |
| Describe the bug | Yes | |
| Steps to reproduce | Yes | Numbered list |
| Expected behavior | Yes | |
| OS / Browser / Version | Yes | |
| Screenshots | No | |
| Additional context | No | |
| Labels | No | Suggest `bug` |
| Assignees | No | |
| Milestone | No | |

**New feature fields:**

| Field | Required | Notes |
|-------|----------|-------|
| Title | Yes | Prefix with `[FEATURE]` |
| Brief description | Yes | |
| Requirements | Yes | Bullet list |
| Acceptance criteria | Yes | Include tests, docs |
| References | No | External links |
| Labels | No | Suggest `enhancement` |
| Assignees | No | |
| Milestone | No | |

**Blank issue fields:**

| Field | Required | Notes |
|-------|----------|-------|
| Title | Yes | Clear, concise summary |
| Body | No | Free-form markdown; omit if not provided |
| Labels | No | |
| Assignees | No | |
| Milestone | No | |

### 3. Determine the repository

Always use the repository of the current workspace. Detect it automatically from the Git remote **before running any `gh` command** and store it for reuse:

```sh
git remote get-url origin
```

If the command exits with a non-zero code or returns no output, stop and tell the user: "No Git remote named `origin` was found in this workspace. Please run this skill from inside the repository where you want to create the issue."

Do **not** ask the user to provide or override the repository. All `gh` commands must use the detected `owner/repo` via `--repo owner/repo`.

### 4. Compose the body

Build the body string following the exact section headings from the template. Write it to a temporary file before calling `gh issue create` to avoid quoting issues. Use the command that matches your shell:

**Windows (PowerShell):**

```powershell
$body = @'
<body content — see template below>
'@
Set-Content -Path "$env:TEMP\issue-body.md" -Value $body -Encoding UTF8
```

**macOS / Linux (bash/zsh):**

```sh
cat > /tmp/issue-body.md << 'EOF'
<body content — see template below>
EOF
```

Then pass the temp file to `gh issue create`:
- Windows: `--body-file "$env:TEMP\issue-body.md"`
- macOS/Linux: `--body-file /tmp/issue-body.md`

**Bug report body:**

```markdown
### Describe the bug
<description>

### To Reproduce
Steps to reproduce the behavior:
1. <step 1>
2. <step 2>
3. <step 3>
4. See error

### Expected behavior
<expected behavior>

### Screenshots
<screenshots or "N/A">

### Desktop (please complete the following information):
 - OS: <os>
 - Browser: <browser>
 - Version: <version>

### Additional context
<additional context or "N/A">
```

**New feature body:**

```markdown
<brief description>

### Requirements:
- <requirement 1>
- <requirement 2>

### Acceptance Criteria:
- Add tests
- Add documentation
- <other criteria>

### References
- <link or "N/A">
```

### 5. Create the issue

Use `--body-file` to pass the composed body. Only add `--label` if the label already exists in the repository (verify with `gh label list`). `--body-file` and `--template` are mutually exclusive in the GitHub CLI — do not pass both.

**Bug report:**

```powershell
# Windows (PowerShell)
gh issue create `
  --title "[BUG] <short description>" `
  --body-file "$env:TEMP\issue-body.md" `
  --label "bug" `  # only if the label exists
  --repo "owner/repo"
```

```sh
# macOS / Linux
gh issue create \
  --title "[BUG] <short description>" \
  --body-file /tmp/issue-body.md \
  --label "bug" \  # only if the label exists
  --repo "owner/repo"
```

**New feature:**

```powershell
# Windows (PowerShell)
gh issue create `
  --title "[FEATURE] <short description>" `
  --body-file "$env:TEMP\issue-body.md" `
  --label "enhancement" `  # only if the label exists
  --repo "owner/repo"
```

```sh
# macOS / Linux
gh issue create \
  --title "[FEATURE] <short description>" \
  --body-file /tmp/issue-body.md \
  --label "enhancement" \  # only if the label exists
  --repo "owner/repo"
```

For blank issues: if the user provides a body, pass it with `--body "<text>"`. If the user provides no body, omit all body flags entirely. Do not use `--body-file` for blank issues.

```sh
# Title only
gh issue create \
  --title "<short description>" \
  --repo "owner/repo"

# Title + body
gh issue create \
  --title "<short description>" \
  --body "<free-form description>" \
  --repo "owner/repo"
```

Add `--label` once per label when the user requests multiple labels (e.g., `--label "bug" --label "good first issue"`). Add `--assignee` once per assignee and `--milestone "<name>"` when provided.

### 6. Confirm and report

After the command succeeds, `gh` prints the URL of the created issue. Share it with the user:

```
https://github.com/owner/repo/issues/123
```

## Available Labels (BitBlazor defaults)

Check existing labels before assigning:

```sh
gh label list --repo owner/repo
```

Common labels used in this repository:

| Label | Use When |
|-------|----------|
| `bug` | Something is not working as expected |
| `enhancement` | New feature or improvement request |
| `documentation` | Documentation is missing or incorrect |
| `good first issue` | Suitable for new contributors |
| `help wanted` | Extra attention or community help needed |
| `question` | Further clarification is needed |
| `components` | issues related to the components |
| `CI\CD` | issues related to the CI\CD pipelines |

## Useful Supplementary Commands

```sh
# List open issues
gh issue list

# View a specific issue
gh issue view 42

# Close an issue
gh issue close 42 --comment "Resolved in PR #99"

# Reopen an issue
gh issue reopen 42

# List available milestones
gh api repos/{owner}/{repo}/milestones --jq '.[].title'

# List available assignable users
gh api repos/{owner}/{repo}/collaborators --jq '.[].login'
```

## Error Handling

| Error | Fix |
|-------|-----|
| `gh: command not found` | Install GitHub CLI: https://cli.github.com |
| `You are not logged into any GitHub hosts` | Run `gh auth login` |
| `Label not found` | Run `gh label list` to see valid labels; create missing ones with `gh label create` |
| `No such milestone` | Run `gh api repos/{owner}/{repo}/milestones` to list milestones |
| `Assignee is not a collaborator` | Run `gh api repos/{owner}/{repo}/collaborators --jq '.[].login'` to list valid assignees, then re-run with a valid `--assignee` value |
| `Repository not found` | Ask the user to verify the `owner/repo` slug and re-run with the corrected value using `--repo "<corrected-owner/repo>"` |
| `Permission denied` | Ensure the authenticated user has write access to the repository |
