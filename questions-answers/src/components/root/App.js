import { Container } from "reactstrap"
import Navi from "../navi/Navi"
import QuestionList from "../posts/QuestionList"

function App() {
  return (
    <div>
      <Navi />
      <Container>
        <QuestionList />
      </Container>
    </div>
  );
}

export default App;
