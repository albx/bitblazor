<!--- Automatically added documentation for repository agents, instructions and skills --->
# Agents, Instructions & Skills

This document lists the repository-scoped agents, instruction files, and skills available to contributors and automation tooling in this workspace.

## Purpose
- Provide a concise reference for contributors who want to use or extend the developer agents and skills.

## Agents
Below are repository-level agents available for local development tasks and automation. Use these agents when you need focused functionality (code review, feature development, etc.).

- `code-reviewer`: Reviews BitBlazor components and PR diffs for accessibility and style.
- `developer`: Implements features, writes bUnit tests, and edits BitBlazor components.
- `projectmanager`: Files and manages GitHub issues and feature requests.
- `software-architect`: Produces high-level designs for new components/features.

## Instruction Files
Repository-level instruction files define policies and authoring patterns applied across the codebase. Key instruction files in this repo:

- `.github/instructions/a11y.instructions.md`: Accessibility standards (WCAG 2.2 AA) and anti-patterns used across BitBlazor.
- `.github/instructions/blazor.instructions.md`: Blazor component authoring patterns and conventions for BitBlazor components.

Contributors should consult these instruction files when working on UI components, accessibility fixes, or Blazor-specific code.

## Skills
Reusable skills (smaller capability units used by agents) present in the environment include (non-exhaustive):

- `blazor`: Author and review BitBlazor library components and bUnit tests.
- `issue-reader` / `issue-writer`: Read and create GitHub issues.

## How to extend
- To add a new agent: follow the existing agent patterns, add corresponding SKILL.md or agent description, and update this document.
- To add a skill or instruction file: place the file under a clear path (for example `.github/instructions/` for policy files or `.agents/skills/` for skill manifests) and reference it in the developer docs.

## Where to find the source
- Instruction files: `.github/instructions/`
- Agent definitions: `.github/agents/`
- Skill definitions: `.agents/skills/`
---
This doc is intended as a lightweight reference. For details, consult the instruction files and skill manifests in the workspace.
