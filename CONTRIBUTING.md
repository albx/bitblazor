# Contributing to BitBlazor

Thank you for your interest in contributing to BitBlazor! We welcome contributions from the community to help improve and grow the project. Please follow the steps below to get started:

## 1. Fork the Repository
- Click the "Fork" button at the top right of the repository page to create your own copy.

## 2. Clone Your Fork
- Clone your forked repository to your local machine:
  ```pwsh
  git clone https://github.com/<your-username>/bitblazor.git
  ```

## 3. Create a Branch
- Create a new branch for your feature or bugfix:
  ```pwsh
  git checkout -b feature/your-feature-name
  ```

## 4. Set Up the Development Environment
- Ensure you have [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) installed.
- Open the solution file `BitBlazor.sln` in Visual Studio or your preferred IDE.
- Restore dependencies:
  ```pwsh
  dotnet restore
  ```

## 5. Make Your Changes
- Implement your feature, bugfix, or documentation update.
- Follow the existing code style and conventions.
- Add or update tests as needed in the `tests/BitBlazor.Test/` folder.

## 6. Run Tests
- Run all tests to ensure your changes do not break anything:
  ```pwsh
  dotnet test
  ```

## 7. Commit and Push
- Commit your changes with a clear message:
  ```pwsh
  git add .
  git commit -m "Describe your changes"
  git push origin feature/your-feature-name
  ```

## 8. Create a Pull Request
- Go to your fork on GitHub and click "Compare & pull request".
- Fill out the PR template, describing your changes and referencing any related issues.
- Submit the pull request to the `main` branch.

## 9. Review Process
- Your pull request will be reviewed by maintainers.
- You may be asked to make changes or provide additional information.
- Once approved, your changes will be merged.

## 10. Code of Conduct
- Please be respectful and follow the [Code of Conduct](CODE_OF_CONDUCT.md) when interacting with others.

## Additional Notes
- For major changes, please open an issue first to discuss what you would like to change.
- Make sure your code passes all checks and tests before submitting a PR.
- Documentation improvements are always welcome!

Thank you for contributing to BitBlazor!
