import onnx
from skl2onnx import convert_sklearn
from skl2onnx.common.data_types import FloatTensorType

from sklearn.tree import DecisionTreeClassifier
from sklearn.model_selection import train_test_split
import pandas as pd

# Load the dataset
df = pd.read_csv('processed_dataset.csv')

# Prepare the data for decision tree classification
y = df['fraud']
X = df.drop(columns=['transactionId', 'fraud'])
X = pd.get_dummies(X, drop_first=True)

# Split the dataset into training set and testing set
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.3, random_state=4)

# Create Decision Tree classifier object
clf = DecisionTreeClassifier()

# Train Decision Tree Classifier
clf = clf.fit(X_train, y_train)

# Print accuracy
print(f'Accuracy:\t{clf.score(X_test, y_test)}')

# Converting the model to ONNX format
initial_type = [('float_input', FloatTensorType([None, X_train.shape[1]]))]
onnx_model = convert_sklearn(clf, initial_types=initial_type)

# Saving the model
with open("decision_tree_model.onnx", "wb") as f:
    f.write(onnx_model.SerializeToString())
