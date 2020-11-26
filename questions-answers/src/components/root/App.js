import { Container } from "reactstrap"
import Navi from "../navi/Navi"
import QuestionList from "../posts/QuestionList"
import Login from "../user/Login"
import Register from "../user/Register"
import { Route, Switch } from "react-router-dom"

function App() {
  return (
    <div>
      <Navi />
      <Container>
        <Switch>
          <Route path="/" exact component={QuestionList} />
          <Route path="/auth/login" exact component={Login} />
          <Route path="/auth/register" exact component={Register} />
        </Switch>
      </Container>
    </div>
  );
}

export default App;
