` 키를 누르면 카메라 시점 전환이 됩니다.(키보드 상단 숫자키 1 왼쪽에 있는 키)

케이크 아이템을 먹으면 속도가 10초간 5배 부스트됩니다.

모닥불 옆에 있는 동그란 원판은 점프대입니다.

물가에는 움직이는 플랫폼이 있습니다.

-------------------------------------------------------------------------
꾸준실습<br>
Q1. 숙련 1강 ~ 숙련 3강<br><br>

Q1-1<br>
입문 주차와 비교해서 입력 받는 방식의 차이와 공통점을 비교해보세요.<br>
  A.<br>
  입문주차의 입력 받는 방식:<br>
  인풋시스템의 SendMessage기능을 통해 전역적으로 메서드에 접근한뒤, 문자열을 비교해서 메서드를 호출하는 방식이다.<br>
  런타임 성능에 비효율적일 수있다.<br>
  SendMessage를 통해 Action Event가 실행되면 해당 Action을 구독중인 각각의 메서드가 호출되는 방식이다.<br><br>
    
  숙련주차의 입력 받는 방식:<br>
  InputSystem과 Event를 직접 연결해서 특정 메서드를 호출한다. 성능이 좋고 관리가 쉽다.<br><br>
  
  공통점은 둘다 InputSystem을 통해 입력받는다는 점.<br>
<br>
<br>
Q1-2 <br>
`CharacterManager`와 `Player`의 역할에 대해 고민해보세요.<br>
  A.<br>
  플레이어는 controller, condition, equip, itemData의 정보를 갖고있다.<br>
  그러한 플레이어의 정보를 CharacterManager가 들고있다.<br>
  외부에서 CharacterManager를 통해 전역적으로 플레이어의 정보에 접근할 수 있게해주는 역할.<br>
<br>
  그래서 외부에서 플레이어의 condition에 접근한다면,<br>
  외부 -> characterManager -> player -> condition으로 접근할 수 있게된다.<br>
  <br>
Q1-3<br>
핵심 로직을 분석해보세요 (`Move`, `CameraLook`, `IsGrounded`)<br>
  A. <br>
     MOVE:<br>
     InputSystem에서 WASD키값에 각각의 이벤트를 연결하여 등록해놓는다.<br>
     키입력이 들어갈때마다 벡터값이 변하고 그 값을 통해, 리지드바디의 속도값에다가 Vector3값을 넣어주는 함수를<br>
     지속적으로 FixedUpdate해주게 됨으로써 플레이어가 이동하게된다.     <br>
  A.<br>
     CameraLook:<br>
     InputSystem에서 마우스델타값 변화에 따라 호출되는 이벤트를 연결하여 등록해놓는다.<br>
     마우스델타값이 변할 때 카메라의 각도도 변하게 한다. <br>
  A.<br>
     IsGrounded:<br>
     플레이어의 아래쪽으로 레이를 4개 쏴서, 레이어마스크를 식별한다. Ground레이어마스크인지 여부를 true false로 반환한다.   <br>
  <br>
Q1-4 <br>
`Move`와 `CameraLook` 함수를 각각 `FixedUpdate`, `LateUpdate`에서 호출하는 이유에 대해 생각해보세요.<br>
  A. Update는 매 프레임마다 호출된다. 그래서 초당 프레임 수에 따라서 호출 빈도가 달라진다. <br>
    저사양 컴퓨터는 초당 60프레임, 고급 컴퓨터는 초당 120프레임이라면, 고급 컴퓨터로는 두배 더 많은 호출을 할 수 있게된다.<br>
    달리기 게임이라고 치면 두배 더 멀리 갈 수 있는거다. 형평성이 떨어진다.<br>
    반면 FixedUpdate는 프레임에 상관없이 시간 간격에 따라 호출된다. 프레임이 어떻던 간에 고정된 0.02초마다 실행되기 때문에 컴퓨터간의 차이에도 형평성의 문제가 없다.<br>
    물리 엔진 관련 작업에 적합하다. <br>
    LateUpdate는 모든 Update가 끝난 후에 호출된다. 플레이어 오브젝트가 update에서 위치를 변경하면 그 이후에 카메라는 그 위치에 맞춰서 따라가는 거다.<br>
    <br>
<br>
Q2-1<br>
- 별도의 UI 스크립트를 만드는 이유에 대해 객체지향적 관점에서 생각해보세요.<br>
  A. UI와 관련된 코드가 한 군데에 모여있으면 코드가 복잡해지지 않는다. 가독성이 좋아진다.<br>
     단일 책임 원칙에 따라, UI의 변화에만 집중하게 한다.<br>
     UI 컴포넌트로 정의되어있기 때문에, 다른 씬이나 다른 UI에서도 코드 중복 없이 재사용할 수 있다.<br>
     모듈화되어있기 때문에 코드를 중복해서 새로 구현하지 않고 재사용할 수 있다.<br>
     UI로직이 별도로 분리되어있다면 디버깅 테스트하기 쉽다.<br>
     <br>
Q2-2<br>
- 인터페이스의 특징에 대해 정리해보고 구현된 로직을 분석해보세요.<br>
  A. 구현할 메서드와 속성의 큰그림을 잡아준다. 특정한 기능을 강제로 정의해야하게 만든다.<br>
     추상화 추상화라는 말은 어떻게든 구현을 하긴해야하는데 구현의 디테일한 방식은 클래스 몫으로 맡겨 버리는 거다.<br>
     클래스와 달리 다중상속이 가능하다. Idamagable, IMovable과 같은 인터페이스를 동시에 상속 받을 수 있다.<br>
     인터페이스 조합에 따라 다양한 기능을 쓸 수 있게 한다.<br>
     객체간의 상호작용이 표준화되므로 코드가 확장되거나 수정되어도 간단하게 유지보수 가능.<br>
<br>
     IInteractable 인터페이스에는 GetInteractPrompt()와 OnInteract()가 있다. <br>
     이 인터페이스를 상속받은 ItemObject는 반드시 위 두가지 매서드를 구현해야하는데, 구현만 한다면 어떻게 구현하던 클래스 몫이다.<br>
     IDamageable 인터페이스에는 TakePhysicalDamage가 있고 이를 상속받은 PlayerCondition은 반드시 저 메서드를 구현해야한다.<br>
<br>
     여러 클래스들에 공통으로 들어갈 걸로 예상되는 속성, 즉 메서드를 인터페이스가 들고 있는 거다. 그리고 메서드를 반드시 구현하도록 강제하는 게 인터페이스다.<br>
<br>
Q2-3<br>
- 핵심 로직을 분석해보세요. (UI 스크립트 구조, `CampFire`, `DamageIndicator`)<br>
  A. Condition 클래스를 통해 플레이어가 아니더라도 모든 클래스들의 <br>
     다양한 상태(예: 체력, 스태미나, 허기, 에너지 레벨)를 관리하고 UI를 업데이트한다.<br>
     Condition 데이터가 정의된 Condition 클래스. 여기서는 값의 증감을 관리하고 UI상에 퍼센트가 반영되도록 한다.<br>
     PlayerCondition에서는 플레이어의 전체상태를 관리한다. 값을 지속적으로 깎거나 추가하는 등 업데이트한다.<br>
     값과 상태를 관리하여 플레이어 상태를 관리한다.<br>
     uiCondition은 Condition의 객체들을 묶어서 UI에서 한번에 관리할 수 있도록 한다.<br>
     Condition에 있는 health, hunger, stamina 각각의 상태를 PlayerCondition클래스가 간접적으로 접근할 수 있게 했다.<br>
<br>
  A. CampFire는 OnTriggerEnter될 경우 IDamageable을 갖고있는 콜라이더의 체력을 깎는다.<br>
<br>
  A. DamageIndicator는 onTakeDamage이벤트가 외부에서 실행될 경우, Flash()메서드가 호출되어 화면에 빨간 플래시 효과를 표시하는 구조다.<br>
     코루틴을 사용하여 시간이 지날 때 자연스럽게 색깔이 변화하도록 하였다.(연속적인 변화처리)<br>
<br>
<br>
### Q3. 숙련 9강 ~ 숙련 11강<br>
- `Interaction` 기능의 구조와 핵심 로직을 분석해보세요.<br>
- `Inventory` 기능의 구조와 핵심 로직을 분석해보세요.<br>
Q3-1<br>
  A. Interaction: 지속적으로 카메라 스크린 중앙에 Ray를 쏴서 IInteractable 인터페이스를 갖고있는 object인지 확인한다. 만약 맞다면, 그 오브젝트의 정보를 캐싱해온다.(가져온다) 화면상에는 그 object를 설명하는 Text를 띄운다. E키를 누르면 그 객체와 상호작용이 실행된다.<br>
     <br>
Q3-2<br>
  A. Inventory는 UI관련 기능을 관리하고, 아이템을 표시하고, 아이템 사용 장착 버리기 동작을 제어한다.<br>
    아이템 관리, UI업데이트, 인터렉션을 제어한다.<br>
    slots 배열과 slotPanel트랜스폼을 사용해 인벤토리의 각 슬롯을 관리한다. slots에는 아이템을 담을 수 있게 해놓은 ItemSlot배열을 통해<br>
    인벤토리 창의 각 위치에 아이템 정보를 표시한다.<br>
    선택된 아이템의 이름과설명,속성등의 정보를 표시한다.<br>
    아이템의 타입과 장착상태에 따라 usebutton, equip, unequip, drop 버튼을 활성화한다.<br>
    Start메서드에서 CharacterManager.Instance.Player.controller.Inventory와 CharacterManager.Instance.Player.addItem <br>
    이벤트에 구독하여 인벤토리를 여닫는 기능과 아이템 추가 기능을 초기화한다.<br>

------------------------------------------------------------------------

