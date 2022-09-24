# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased] - 2022-06-21

### Added 
- New `MoveSystem` class.
- New `VelocityComponent` class.
- New `PositionComponent` class.
- Input: `InputChanged` event alternative for `GetCommand`.

### Changed
- BREAKING: Upgrade target framework to `net6`
- Rename namespace EC to ECS since we are shifting into Entity Component System architecture.

## [1.0.0] - 2022-06-16

### Added
- Initial release.
