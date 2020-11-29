import { Container } from "reactstrap"
import Navi from "../navi/Navi"
import QuestionList from "../posts/QuestionList"
import Login from "../auth/Login"
import Register from "../auth/Register"
import User from "../user/User"
import QuestionDetail from "../posts/QuestionDetail"
import AddQuestion from "../posts/AddQuestion"
import { Route, Switch } from "react-router-dom"

function App() {
  return (
    <div>
      <Navi />
      <Container className="pt-5">
        <Switch>
          <Route path="/" exact component={QuestionList} />
          <Route path="/question/add" exact component={AddQuestion} />
          <Route path="/question/:id" exact component={QuestionDetail} />
          <Route path="/auth/login" exact component={Login} />
          <Route path="/auth/register" exact component={Register} />
          <Route path="/user/:id" exact component={User} />
          <Route path="/user/" exact component={User} />
        </Switch>
      </Container>
    </div>
  );
}

export default App;
