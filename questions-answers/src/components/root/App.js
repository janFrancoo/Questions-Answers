import { Container } from "reactstrap"
import Navi from "../navi/Navi"
import QuestionList from "../posts/QuestionList"
import Login from "../auth/Login"
import Register from "../auth/Register"
import User from "../user/User"
import Settings from "../user/Settings"
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
          <Route path="/user/settings" exact component={Settings} />
          <Route path="/user/:id" exact component={User} />
          <Route path="/user/" exact component={User} />
        </Switch>
      </Container>
    </div>
  );
}

export default App;
