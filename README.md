# Mini Game Paradise

## 목표
미니게임천국 모작 만들기
## 개발 기간
2023.04 ~ 2023.07
## 직접 구현 로직
1. 3개를 초과하는 블럭 매치 발생 시 특수 아이템 블록 생성
2. 아이템 블록 사용 시 발생하는 상황에 대한 처리
3. 게임 매니저를 통한 게임 성공 및 게임 실패 로직 구현
   - 남은 횟수, 남은 블록 수를 비교한 판단
## 사용 디자인 패턴
1. 싱글턴
   - 씬 이동 간 필요한 사운드 이펙트 재생, 게임룰 구현을 담당할 사운드 매니저, 게임 매니저 생성


2. 오브젝트 풀링
   - 블록 파괴 시 생성하는 폭발 이펙트 오브젝트를 필요한만큼만 생성하고 사용이 완료된 오브젝트를 재사용

