# SODV2452-01 Application Development — Assignment Reference

Course: [Application Development (SODV2452-01)](https://d2l.bowvalleycollege.ca/d2l/home/481525)
Source: Brightspace D2L Dropbox/Assignment Folders + Rubrics (checked 2026-08-05)

## IMPORTANT: where each deliverable actually lives

- **Assignment 3** (Module 3, simple unit-testing exercise — own choice of class, NUnit tests
  covering normal/edge/incorrect/exception/empty-string cases) → **this workspace**
  (`BankAccountLibrary` + `BankAccountLibrary.Tests`, this repo `AppDEV Assignment 3 and 4`).
  Fully implemented: 28/28 tests passing (see `BankAccountLibrary.Tests/BankAccountTests.cs`).
  Only the zip + demo video (showing tests passing) are still outstanding for submission.
- **Project 3 / Project 4 / Assignment 4** (the bigger rubric items tied to "Awesome Chat, the
  project you developed in Project 2") → the REAL repo is
  `D:\Current School Projects\SchoolWork-AppDev\Projects\AwesomeChat`, **not** anything in this
  workspace. A `BankAccountConsoleApp` was built here as a practice exercise for the Project
  3/4 rubric skills but is **not** the submission target — Assignment 4's official text names
  "Awesome Chat" specifically.

### AwesomeChat status for Project 3 / Project 4 / Assignment 4 (checked 2026-08-05)

| Deliverable | Status | Evidence |
|---|---|---|
| Unit tests (Conduct testing) | ✅ Done | 25 tests passing, 53% line / 64% branch coverage |
| DCR (Design Change Request) | ✅ Done | `AwesomeChat.Tests/DCR.md` |
| Debugging tools notes | ✅ Done | `AwesomeChat.Tests/DebuggingNotes.md` |
| Pre-made library (Serilog) w/ justification | ✅ Done | README "Library: Serilog" section |
| Log examination / root cause analysis | ✅ Done | README "Log Analysis" section |
| Installer (.exe) | ✅ Done | `Installer/Output/AwesomeChatSetup.exe`, built via `Installer/AwesomeChat.iss` |
| Installer tested on multiple machines | ✅ Done | README "Installer Test Results" table (laptop + desktop) |
| Installer guide (end-user text file) | ✅ Done (added 2026-08-05) | `Installer/InstallerGuide.txt` |
| Inline documentation + README | ✅ Adequate | README is extensive; code has class-level XML doc comments explaining design + key inline comments on non-obvious logic (atomic write, TCP fragmented-read loop) |
| **Demo video (install → run → uninstall)** | ❌ Not done | No `.mp4`/`.mov` found anywhere in the repo — still needs to be recorded |
| Reflection Report | ✅ Written, not yet submitted | `Assignments/ReflectionReport/chongtham_eddie_reflection_report.{html,pdf}` |

**The only remaining blocker for Project 3/4 + Assignment 4 submission is recording the demo
video.** Everything else (code, tests, DCR, debugging notes, installer, installer guide,
documentation) already exists in the AwesomeChat repo.

## Status Snapshot (as of 2026-08-05)

| Assignment | Due | Status | Score |
|---|---|---|---|
| Project 1. Analyze the scope of the task(s) and user requirements to address software needs. | Jul 26, 2026 11:59 PM | Submitted | 12 / 12 - A+ |
| Project 2. Implement the desired features and functionality, with attention to detail. | Aug 2, 2026 11:59 PM | Submitted | 0 / 6 - F |
| Project 3. Test the software to ensure it is bug-free and performs as expected demonstrated with adaptability | Aug 16, 2026 11:59 PM | Not Submitted | - / 9 |
| Project 4. Release packaged software while demonstrating written communication skills | Aug 20, 2026 11:59 PM | Not Submitted | - / 12 |
| Professionalism / Reflection Report | Aug 20, 2026 11:59 PM | Not Submitted | - / 5 |

## Project 3 — Test the software to ensure it is bug-free and performs as expected demonstrated with adaptability

**Due: Aug 16, 2026, 11:59 PM** | Worth **9 points** (3 criteria x 3 pts)

| Criterion | Mastery (3) | Competent (2) | Developing (1) | Incomplete (0) |
|---|---|---|---|---|
| **Conduct testing** | All competent criteria AND: covers a % of code (e.g. 50-60%, depends on team/project — 100% is overkill) | ALL of: design meaningful unit tests; ensure new features don't break other components | Did not meet all competent-level criteria | Submitted evidence was incomplete |
| **Implement DCR (Design Change Request)** | All competent criteria AND: changes the unit tests to make it work | ALL of: ensure new features don't break other components; ensure code passes unit tests; ensure code coverage meets org standards; address code coverage results | Did not meet all competent-level criteria | Submitted evidence was incomplete |
| **Use debugging tools** | All competent criteria AND: covers a % of code (e.g. ~80%, depends on team — 100% is overkill) | ALL of: step into each line of code; set breakpoints; observe data/variable assignments in the IDE | Did not meet all competent-level criteria | Submitted evidence was incomplete |

## Project 4 — Release packaged software while demonstrating written communication skills

**Due: Aug 20, 2026, 11:59 PM** | Worth **12 points** (4 criteria x 3 pts)

| Criterion | Mastery (3) | Competent (2) | Developing (1) | Incomplete (0) |
|---|---|---|---|---|
| **Use pre-made libraries** | All competent criteria AND: can explain pros/cons/limitations of the chosen library (justify selection) | ALL of: research libraries serving the functionality; integrate the chosen library into the project | Did not meet all competent-level criteria | Submitted evidence was incomplete |
| **Examine Logs** | All competent criteria AND: pinpointed the problem to find the root cause | ALL of: identify sequence of events; view logs for problem-solving | Did not meet all competent-level criteria | Submitted evidence was incomplete |
| **Package project output to an installer** | All competent criteria AND: tested on multiple machines to verify it works | ALL of: add binaries/dependencies to installer; set installer metadata; test installer; uninstall application | Did not meet all competent-level criteria | Submitted evidence was incomplete |
| **Update documentation for relevance** | All competent criteria AND: tested on multiple machines to verify it works | ALL of: update ReadMe (if needed); update installation docs (if needed); update user manuals (if needed) | Did not meet all competent-level criteria | Submitted evidence was incomplete |

### Assignment 4 — official text (Content: Table of Contents › Learning Pathway › Module 4: Release Packaged Software while Demonstrating Written Communication Skills › Assignment 4)

> For the last part of the assignment, you will create the following for the Awesome Chat, you have developed in Project 2:
> 1. Create an installer (.exe file) for Awesome Chat.
> 2. Create an installer guide for the end users on how to install the client chat application.
> 3. Create inline documentation and a README file for the project.
>
> **Submission:** Upload a zip file having the above file, and a video demonstrating the successful installation of the application.

### Assignment 4 concrete deliverables (from Module 4 video walkthrough, instructor's own words)

For **Awesome Chat** (the project built in Project 2), create:

1. **Installer** — a `.exe` installer for Awesome Chat.
2. **Installer guide** — a simple text file for end users explaining how to install the client chat application (this is distinct from the user manual).
3. **Inline documentation + Readme** — code-level (inline) documentation, plus a project Readme file.

**Submission format:** one zip file containing the installer, installer guide, inline-documented code/Readme, **and** a video demonstrating successful installation. The video only needs to show: install → run → uninstall. No need to show how the setup project itself was built.

### How to build the installer (Visual Studio 2022 steps from the video)

1. Install the **Microsoft Visual Studio Installer Projects** extension (Extensions → Manage Extensions).
2. In Solution Explorer: right-click the solution → Add → New Project → search "setup" → choose **Setup Project** → name it (e.g. `Setup1`) → Create.
3. Right-click the setup project → View → File System.
4. Right-click **Application Folder** → Add → **Primary Output** → select the project's primary output. This adds the app's files, but **not** a `.mdf` database file if one is used.
5. If a `.mdf` database file is used: Add → Files → browse to and select the `.mdf` file, then add it to the Application Folder as well.
6. Set setup project properties (bottom of Properties window, alphabetical list): Author/Manufacturer (e.g. "Bow Valley College"), Description, Product Name, etc.
7. Right-click the setup project → **Build**. Output `.exe`/installer files land in the project's `Debug`/`Release` output folder.
8. Test by running the generated setup file: install, run the app, then uninstall it.

Background links the instructor referenced (search these titles in Content if needed):
- "Create setup.exe in Visual Studio 2022 Step by Step" — recommended primary reference, works for a simple Windows Forms app with no DB dependency.
- "Create Setup File With attaching SQL Database .mdf with proof in C# Windows Application" — old VS2010-based guide; still useful for the general idea of attaching a `.mdf`, but follow the VS2022 steps above and just make sure to add the `.mdf` file during the Add → Files step.

### Suggested Readme structure (from the video's example)

- Technologies (e.g. C#)
- About (short description of the project)
- Prerequisites (e.g. Visual Studio 2022, .NET Framework version)
- How to clone the repository (skip if Git wasn't used)
- How to run the solution

## Notes

- **Recommended order: Project 3 before Project 4.** Not a hard D2L-enforced dependency, but both build on the same Awesome Chat codebase from Project 2, due dates run Project 3 (Aug 16) → Project 4 (Aug 20), and Project 4's installer/packaging should reflect the debugged, tested version of the app rather than the raw Project 2 build.
- The Content module for each project (Module 3 and Assignment 3 / Module 4 and Assignment 4) contains a video topic per module; the Module 4 video itself has the full assignment instructions (see above) even though the Content page listing only showed a "Video" topic type.
- Professionalism / Reflection Report (5 pts, due Aug 20, 2026) is a separate dropbox folder, not yet submitted.
- Dropbox folder links (require login):
  - Project 3: `/d2l/lms/dropbox/user/folder_submit_files.d2l?db=300716&grpid=0&isprv=0&bp=0&ou=481525`
  - Project 4: `/d2l/lms/dropbox/user/folder_submit_files.d2l?db=300717&grpid=0&isprv=0&bp=0&ou=481525`
  - Professionalism/Reflection Report: `/d2l/lms/dropbox/user/folder_submit_files.d2l?db=300718&grpid=0&isprv=0&bp=0&ou=481525`
